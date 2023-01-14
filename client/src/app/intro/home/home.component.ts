import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  currentUser$ = this.accountService.currentUser$.pipe(
    map(user => {
      if (user) {
        return user;
      }
      return false;
    })
  );

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

}
