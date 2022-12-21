import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any = { };
  isMenuCollapsed = true;

  isRegistered$ = this.accountService.currentUser$.pipe(
    map(user => {
      if (user) {
        return user.isRegistered;
      }
      return false;
    })
  );

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  logout() {
    this.accountService.logout();
    this.router.navigate(['/']);
  }

}
