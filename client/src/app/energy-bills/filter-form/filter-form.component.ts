import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { selectStatus, selectType } from 'src/app/models/formSelectModels';
import { UserParams } from 'src/app/models/userParams';
import { UserBillsService } from 'src/app/services/user-bills.service';

@Component({
  selector: 'app-filter-form',
  templateUrl: './filter-form.component.html',
  styleUrls: ['./filter-form.component.css']
})
export class FilterFormComponent implements OnInit {
  showFilters = false;
  userParams = new UserParams();
  selectTypes = selectType;
  selectStatuses = selectStatus;
  @Output() onUserParamsChange = new EventEmitter<UserParams>();

  filterForm = this.fb.group({
    type: [''],
    status: [''],
    minDate: [new Date(2022, 0)],
    maxDate: [new Date()]
  });

  constructor(private fb: FormBuilder, private userBillsService: UserBillsService) { }

  ngOnInit(): void {
    this.filterForm.valueChanges.subscribe({
      next: form => {
        if (form.type) this.userParams.type = form.type;
        if (form.status) this.userParams.status = form.status;
        if (form.minDate) {
          this.userParams.minMonth = form.minDate.getMonth() + 1;
          this.userParams.minYear = form.minDate?.getFullYear()
        }
        if (form.maxDate) {
          this.userParams.maxMonth = form.maxDate.getMonth() + 1;
          this.userParams.maxYear = form.maxDate.getFullYear()
        }
        this.onChange();
      }
    });
  }

  onChange() {
    this.onUserParamsChange.emit(this.userParams);
  }

  clear() {
    this.userParams = new UserParams();
    this.filterForm.reset({
      type: '',
      status: '',
      minDate: new Date(2022, 0),
      maxDate: new Date()
    }); 
  }

}
