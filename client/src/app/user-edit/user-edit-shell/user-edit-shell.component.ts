import { Component, OnInit } from '@angular/core';
import { UserParams } from 'src/app/models/userParams';
import { AccountService } from 'src/app/services/account.service';
import { UserBillsService } from 'src/app/services/user-bills.service';

@Component({
  selector: 'app-user-edit-shell',
  templateUrl: './user-edit-shell.component.html',
  styleUrls: ['./user-edit-shell.component.css']
})
export class UserEditShellComponent implements OnInit {
  currentUser$ = this.accountService.currentUser$;

  constructor(private accountService: AccountService, private userBillsService: UserBillsService) { }

  ngOnInit(): void {
    const userParams = new UserParams();
    userParams.state = 'saved';
    this.userBillsService.getUserBills(userParams).subscribe({
      next: response => this.userBillsService.setPagedList(response)
    });
  }

}
