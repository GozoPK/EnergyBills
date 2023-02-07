import { Component, OnInit } from '@angular/core';
import { map, switchMap } from 'rxjs';
import { UserParams } from 'src/app/models/userParams';
import { UserBillsService } from 'src/app/services/user-bills.service';

@Component({
  selector: 'app-user-bills',
  templateUrl: './user-bills.component.html',
  styleUrls: ['./user-bills.component.css']
})
export class UserBillsComponent implements OnInit {
  userParams = new UserParams();

  pagedList$ = this.userBillsService.pagedList$;

  constructor(private userBillsService: UserBillsService) { }

  ngOnInit(): void {
    this.loadUserBills(this.userParams);
  }

  loadUserBills(userParams: UserParams) {
    this.userBillsService.getUserBills(userParams).subscribe({
      next: response => this.userBillsService.setPagedList(response)
    });
  }

  onChange(userParams: UserParams) {
    this.userParams = userParams;
    this.loadUserBills(userParams);
  }

  pageChanged(event: any) {
    if (this.userParams && this.userParams.pageNumber != event) {
      this.userParams.pageNumber = event;
      this.onChange(this.userParams);
    }
  }

}
