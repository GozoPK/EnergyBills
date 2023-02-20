import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ValidatorFn, Validators } from '@angular/forms';
import { CreateBill } from 'src/app/models/createBill';
import { ModalService } from 'src/app/services/modal.service';
import { UserBillsService } from 'src/app/services/user-bills.service';

@Component({
  selector: 'app-add-bill',
  templateUrl: './add-bill.component.html',
  styleUrls: ['./add-bill.component.css']
})
export class AddBillComponent implements OnInit {
  selectTypes = [
    { value: 0, description: 'Ρεύμα'},
    { value: 1, description: 'Φυσικό Αέριο'},
    { value: 2, description: 'Και τα δύο'}
  ]

  createForm = this.fb.group({
    billNumber: ['', [Validators.required, Validators.maxLength(10), Validators.pattern(/^[0-9]+$/)]],
    type: ['', Validators.required],
    date: [new Date()],
    ammount: [0, [Validators.required, this.mustBeGreaterThanZero()]]
  });

  errorMessages$ = this.userBillsService.errorMessages$;

  constructor(private fb: FormBuilder, private userBillsService: UserBillsService, private modalService: ModalService) { }

  ngOnInit(): void {
    this.userBillsService.setErrorMessages(null);
  }

  create() {
    const billForCreation = this.prepareBillForCreation();
    billForCreation.state = 1;

    this.userBillsService.createBill(billForCreation).subscribe({
      next: () => this.modalService.operModal('Επιτυχής Υποβολή')
    });

    this.clear();
  }

  save() {
    const billForCreation = this.prepareBillForCreation();
    billForCreation.state = 0;

    this.userBillsService.createBill(billForCreation).subscribe({
      next: () => this.modalService.operModal('Επιτυχής Αποθήκευση')
    });

    this.clear();
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

  mustBeGreaterThanZero(): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value <= 0 ? { lessThanZero: true } : null;
    }
  }

  clear() {
    this.createForm.reset({
      billNumber: '',
      type: '',
      date: new Date(),
      ammount: 0
    });

    this.userBillsService.setErrorMessages(null);
  }

}
