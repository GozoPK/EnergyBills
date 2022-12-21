import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.css']
})
export class BillsComponent implements OnInit {
  model: any = { };

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.get().subscribe({
      next: response => this.model = response
    });
  }

}
