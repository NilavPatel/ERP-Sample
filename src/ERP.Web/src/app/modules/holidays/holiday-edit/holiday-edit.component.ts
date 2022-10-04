import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { HolidayService } from 'src/app/modules/holidays/shared/holiday.service';
import { LoaderService } from 'src/app/core/services/loader.service';

@Component({
  selector: 'app-holiday-edit',
  templateUrl: './holiday-edit.component.html',
  styleUrls: ['./holiday-edit.component.scss']
})
export class HolidayEditComponent implements OnInit {

  id: string | null = "";
  name: string = "";
  holidayOn: any = null;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private holidayService: HolidayService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.goToList();
    } else {
      this.getHolidayById();
    }
  }

  getHolidayById() {
    this.loaderService.showLoader();
    var req = {
      id: this.id
    };
    this.holidayService.getHolidayById(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.name = value.data.name;
            this.holidayOn = new Date(value.data.holidayOn);
          }
        } else {
          this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
        }
      },
      error: (error: any) => {
        this.messageService.add({ severity: 'error', detail: error.message });
        this.loaderService.hideLoader();
      }, complete: () => {
        this.loaderService.hideLoader();
      }
    });
  }

  goToList() {
    this.router.navigateByUrl("/app/holidays/list");
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      id: this.id,
      name: this.name,
      holidayOn: this.holidayOn
    }
    this.holidayService.updateHoliday(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
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
}
