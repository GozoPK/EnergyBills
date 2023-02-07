import { Component, OnInit } from '@angular/core';
import { UserParams } from 'src/app/models/userParams';
import { BillsService } from 'src/app/services/bills.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  constructor(private billsService: BillsService) { }

  ngOnInit(): void {
    this.billsService.getBills(new UserParams()).subscribe({
      next: response => this.billsService.setPagedList(response)
    });
  }

}
