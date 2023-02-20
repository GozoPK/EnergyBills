import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ValidatorFn, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { ModalService } from 'src/app/services/modal.service';

@Component({
  selector: 'app-user-edit-security',
  templateUrl: './user-edit-security.component.html',
  styleUrls: ['./user-edit-security.component.css']
})
export class UserEditSecurityComponent implements OnInit, OnDestroy {
  changePasswordForm = this.fb.group({
    oldPassword: ['', Validators.required],
    newPassword: ['', [Validators.required, Validators.minLength(10)]],
    confirmPassword: ['', [Validators.required, this.matchPasswords('newPassword')]]
  });

  sub = new Subscription();

  constructor(private fb: FormBuilder, private accountService: AccountService, private modalService: ModalService) { }

  ngOnInit(): void {
    this.accountService.setErrorMessages(null);
    this.sub = this.changePasswordForm.controls['newPassword'].valueChanges.subscribe({
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
      next: () => {
        this.modalService.operModal("Ο Κωδικός σας άλλαξε.")
        this.changePasswordForm.reset();
      }
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
