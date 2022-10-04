import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { HolidayService } from 'src/app/modules/holidays/shared/holiday.service';
import { LoaderService } from 'src/app/core/services/loader.service';

@Component({
  selector: 'app-holiday-add',
  templateUrl: './holiday-add.component.html',
  styleUrls: ['./holiday-add.component.scss']
})
export class HolidayAddComponent implements OnInit {

  name: string = "";
  holidayOn: any = null;

  constructor(private router: Router,
    private holidayService: HolidayService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
  }

  goToList() {
    this.router.navigateByUrl("/app/holidays/list");
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      name: this.name,
      holidayOn: this.holidayOn
    }
    this.holidayService.createHoliday(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.goToList();
          } else {
            this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
          }
        }, error: (error: any) => {
          this.messageService.add({ severity: 'error', detail: error.message });
          this.loaderService.hideLoader();
        }, complete: () => {
          this.loaderService.hideLoader();
        }
      });
  }

  onClearClick(form: NgForm) {
    this.name = "";
    this.holidayOn = null;
    form.resetForm();
  }

}
