import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { ModalService } from 'src/app/services/modal.service';

@Component({
  selector: 'app-user-edit-security',
  templateUrl: './user-edit-security.component.html',
  styleUrls: ['./user-edit-security.component.css']
})
export class UserEditSecurityComponent implements OnInit {
  changePasswordForm = this.fb.group({
    oldPassword: ['', Validators.required],
    newPassword: ['', [Validators.required, Validators.minLength(10)]],
    confirmPassword: ['', [Validators.required, this.matchPasswords('newPassword')]]
  });

  constructor(private fb: FormBuilder, private accountService: AccountService, private modalService: ModalService) { }

  ngOnInit(): void {
    this.changePasswordForm.controls['newPassword'].valueChanges.subscribe({
      next: () => this.changePasswordForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchPasswords(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control.parent?.get(matchTo)?.value  ? null : {notMatching: true}
    }
  }

  changePassword() {
    const model = {...this.changePasswordForm.value };
    this.accountService.changePassword(model).subscribe({
      next: () => this.modalService.operModal("Ο Κωδικός σας άλλαξε.")
    });
  }

}
