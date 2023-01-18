import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-forms-select',
  templateUrl: './forms-select.component.html',
  styleUrls: ['./forms-select.component.css']
})
export class FormsSelectComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() header = '';
  @Input() options: { value: string, description: string}[] = [];

  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }

  constructor(@Self() public ngControl: NgControl) { 
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }

}
