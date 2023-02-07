import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ValidatorFn, Validators } from '@angular/forms';
import { Admin } from 'src/app/models/admin';
import { AdminService } from 'src/app/services/admin.service';
import { ModalService } from 'src/app/services/modal.service';

@Component({
  selector: 'app-create-admin',
  templateUrl: './create-admin.component.html',
  styleUrls: ['./create-admin.component.css']
})
export class CreateAdminComponent implements OnInit {
  createAdminForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(10)]],
    confirmPassword: ['', [Validators.required, this.matchPasswords('password')]],
    email: ['', [Validators.required, Validators.email]]
  });

  errorMessages$ = this.adminService.errorMessages$;

  constructor(private fb: FormBuilder, private adminService: AdminService, private modalService: ModalService) { }

  ngOnInit(): void {
    this.createAdminForm.controls['password'].valueChanges.subscribe({
      next: () => this.createAdminForm.controls['confirmPassword'].updateValueAndValidity()
    })

    this.adminService.setErrorMessages(null);
  }

  matchPasswords(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control.parent?.get(matchTo)?.value  ? null : {notMatching: true}
    }
  }

  create() {
    const model = { ...this.createAdminForm.value } as Admin;
    this.adminService.createUser(model).subscribe({
      next: () => {
        this.modalService.operModal('Επιτυχής υποβολή')
        this.createAdminForm.reset();
      }
    });
  }

  reset() {
    this.adminService.setErrorMessages(null);
    this.createAdminForm.reset();
  }

}
