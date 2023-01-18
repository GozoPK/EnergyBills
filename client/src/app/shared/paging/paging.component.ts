import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-paging',
  templateUrl: './paging.component.html',
  styleUrls: ['./paging.component.css']
})
export class PagingComponent implements OnInit {
  @Input() totalItems?: number
  @Input() itemsPerPage?: number
  @Input() currentPage?: number
  @Output() onPageChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  pageChanged(event: any) {
    this.onPageChanged.emit(event.page);
  }

}
