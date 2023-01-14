import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { Login } from 'src/app/models/login';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userForLogin: Login = { } as Login;

  errorMessages$ = this.accountService.errorMessages$;

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.accountService.setErrorMessages(null);
  }

  login() {
    this.accountService.login(this.userForLogin).subscribe({
      next: () => this.router.navigate(['/'])
    });
  }

}
