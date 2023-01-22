import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { CreateBill } from 'src/app/models/createBill';
import { Months } from 'src/app/models/months';
import { UserBill } from 'src/app/models/userBill';
import { UserBillsService } from 'src/app/services/user-bills.service';

@Component({
  selector: 'app-edit-bill-modal',
  templateUrl: './edit-bill-modal.component.html',
  styleUrls: ['./edit-bill-modal.component.css']
})
export class EditBillModalComponent implements OnInit {
  bill?: UserBill;
  createForm: FormGroup = new FormGroup({});
  billChanged = false;
  selectTypes = [
    { value: 0, description: 'Ρεύμα'},
    { value: 1, description: 'Φυσικό Αέριο'},
    { value: 2, description: 'Και τα δύο'}
  ]
  
  errorMessages$ = this.userBillsService.errorMessages$;

  constructor(public modalRef: BsModalRef, private fb: FormBuilder, 
    private userBillsService: UserBillsService, private toastr: ToastrService) { }

  ngOnInit(): void {
    const month = Months[this.bill!.month as keyof typeof Months];
    let type;

    switch (this.bill?.type) {
      case 'Ρεύμα': {
        type = 0
        break;
      }
      case 'Φυσικό Αέριο': {
        type = 1
        break;
      }
      case 'Ρεύμα, Φυσικό αέριο': {
        type = 2
        break;
      }
      default: {
        type = ''
      }
    }

    this.createForm = this.fb.group({
      billNumber: [this.bill?.billNumber],
      type: [type, Validators.required],
      date: [new Date(this.bill!.year, month, 1)],
      ammount: [this.bill?.ammount, [Validators.required, this.mustBeGreaterThanZero()]]
    });
  
  }

  mustBeGreaterThanZero(): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value <= 0 ? { lessThanZero: true } : null;
    }
  }

  create() {
    const billForCreation = this.prepareBillForCreation();
    billForCreation.state = 1;

    this.userBillsService.updateBill(billForCreation).subscribe({
      next: () => {
        this.toastr.success('Επιτυχής Υποβολή')
        this.billChanged = true;
        this.modalRef.hide();
      }
    });
  }

  save() {
    const billForCreation = this.prepareBillForCreation();
    billForCreation.state = 0;

    this.userBillsService.updateBill(billForCreation).subscribe({
      next: () => {
        this.toastr.success('Επιτυχής Αποθήκευση')
        this.billChanged = true;
        this.modalRef.hide();
      }
    });
    
  }

  prepareBillForCreation() {
    let date = this.createForm.controls['date'].value;
    let month = 1;
    if (date) month = date.getMonth() + 1;

    const typeString = this.createForm.controls['type'].value;
    let type;
    if (typeString) type = +typeString;

    const billForCreation = {
      billNumber: this.createForm.controls['billNumber'].value,
      type: type,
      month: month,
      year: date?.getFullYear(),
      ammount: this.createForm.controls['ammount'].value,
    } as CreateBill;

    return billForCreation;
  }

}
