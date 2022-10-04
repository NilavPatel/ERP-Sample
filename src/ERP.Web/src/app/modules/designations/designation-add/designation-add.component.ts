import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { DesignationService } from 'src/app/modules/designations/shared/designation.service';

@Component({
  selector: 'app-designation-add',
  templateUrl: './designation-add.component.html',
  styleUrls: ['./designation-add.component.scss']
})
export class DesignationAddComponent implements OnInit {

  designationName: string = "";
  description: string = "";

  constructor(private router: Router,
    private designationService: DesignationService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
  }

  goToList() {
    this.router.navigateByUrl("/app/designations/list");
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      name: this.designationName,
      description: this.description
    }
    this.designationService.createDesignation(req)
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
    this.designationName = "";
    this.description = "";
    form.resetForm();
  }
}
