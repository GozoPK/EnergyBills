import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-forms-input',
  templateUrl: './forms-input.component.html',
  styleUrls: ['./forms-input.component.css']
})
export class FormsInputComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() type = 'text';

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
