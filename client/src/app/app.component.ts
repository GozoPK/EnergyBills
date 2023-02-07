import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'Energy Bills';

  sub = new Subscription();

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    const token = localStorage.getItem('token');

    if (token) {
      this.sub = this.accountService.getCurrentUser().subscribe();
    } 
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
  
}
