import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-user-edit-shell',
  templateUrl: './user-edit-shell.component.html',
  styleUrls: ['./user-edit-shell.component.css']
})
export class UserEditShellComponent implements OnInit {
  currentUser$ = this.accountService.currentUser$;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

}
