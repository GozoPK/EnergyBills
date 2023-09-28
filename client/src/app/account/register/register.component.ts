import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TaxisnetUser } from 'src/app/models/TaxisnetUser';
import { AccountService } from 'src/app/services/account.service';
import { ModalService } from 'src/app/services/modal.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  user: TaxisnetUser | undefined;
  isSuccesfulRegister: boolean = false;

  errorMessages$ = this.accountService.errorMessages$;
  
  constructor(private fb: FormBuilder, private accountService: AccountService,
    private router: Router, private modalService: ModalService) { }

  ngOnInit(): void {
    this.user = this.accountService.user;
    this.accountService.setErrorMessages(null);
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(10)]],
      confirmPassword: ['', [Validators.required, this.matchPasswords('password')]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10), Validators.pattern(/^69[0-9]+$/)]],
      firstName: [this.user?.firstName],
      lastName: [this.user?.lastName],
      afm: [this.user?.afm],
      iban: ['', [Validators.required, Validators.minLength(27), Validators.maxLength(27), Validators.pattern(/^GR[0-9]+$/)]],
      addressStreet: [this.user?.addressStreet],
      addressNumber: [this.user?.addressNumber],
      postalCode: [this.user?.postalCode],
      city: [this.user?.city]     
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    });
  }

  matchPasswords(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control.parent?.get(matchTo)?.value  ? null : {notMatching: true}
    }
  }

  register() {
    const model = { ...this.registerForm.value };
    model.annualIncome = this.user?.annualIncome;
    model.taxisnetToken = this.user?.taxisnetToken;

    this.accountService.register(model).subscribe({
      next: () => {
        this.isSuccesfulRegister = true;
        this.modalService.openRegisterModal('Επιτυχής εγγραφή');
      }
    });
  }

  cancel() {
    this.router.navigate(['/']);
  }

}
