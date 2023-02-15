import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-paging-counter',
  templateUrl: './paging-counter.component.html',
  styleUrls: ['./paging-counter.component.css']
})
export class PagingCounterComponent implements OnInit {
  @Input() totalItems = 0;
  @Input() currentPage?: number;
  @Input() itemsPerPage?: number;

  constructor() { }

  ngOnInit(): void {
  }

}
