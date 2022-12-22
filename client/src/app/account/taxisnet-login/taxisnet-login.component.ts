import { Component, OnInit } from '@angular/core';
import { Login } from 'src/app/models/login';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router'

@Component({
  selector: 'app-taxisnet-login',
  templateUrl: './taxisnet-login.component.html',
  styleUrls: ['./taxisnet-login.component.css']
})
export class TaxisnetLoginComponent implements OnInit {
  userForLogin: Login = { } as Login;

  errorMessage$ = this.accountService.errorMessage$;

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    
  }

  login() {
    this.accountService.taxisnetLogin(this.userForLogin).subscribe({
      next: () => this.router.navigate(['/register'])
    });
  }

}
