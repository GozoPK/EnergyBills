import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { ModalService } from 'src/app/services/modal.service';

@Component({
  selector: 'app-user-edit-details',
  templateUrl: './user-edit-details.component.html',
  styleUrls: ['./user-edit-details.component.css']
})
export class UserEditDetailsComponent implements OnInit {
  userDetailsForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10), Validators.pattern(/^[0-9]+$/)]],
    iban: ['', [Validators.required, Validators.minLength(27), Validators.maxLength(27), Validators.pattern(/^GR[0-9]+$/)]]
  });

  constructor(private fb: FormBuilder, private accountService: AccountService, private modalService: ModalService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: user => {
        if (user) {
          this.userDetailsForm.setValue({
            email: user.email,
            phoneNumber: user.phoneNumber,
            iban: user.iban
          });
        }
      }
    })
  }

  saveChanges() {
    const model = { ... this.userDetailsForm.value };
    this.accountService.editUser(model).subscribe({
      next: () => this.modalService.operModal('Επιτυχής αποθήκευση')
    });
  }

}
