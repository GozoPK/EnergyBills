import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Login } from 'src/app/models/login';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userForLogin: Login = { } as Login;
  returnUrl: string | null;

  errorMessages$ = this.accountService.errorMessages$;

  constructor(private accountService: AccountService, private router: Router, private route: ActivatedRoute) { 
    this.returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
  }

  ngOnInit(): void {
    this.accountService.setErrorMessages(null);
  }

  login() {
    this.accountService.login(this.userForLogin).subscribe({
      next: () => {
        if (this.returnUrl)
          this.router.navigateByUrl(this.returnUrl);
        else 
          this.router.navigate(['/']);
      }
    });
  }

}
