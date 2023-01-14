import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Energy Bills';

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    const token = localStorage.getItem('token');

    if (token) {
      if (!this.isExpired(token)) {
        this.accountService.getCurrentUser().subscribe();
      } 
    } 
  }

  isExpired(token: string) {
    const expireDate = (JSON.parse(atob(token.split('.')[1]))).exp;
    return (Math.floor((new Date).getTime() / 1000)) > expireDate;
  }
  
}
