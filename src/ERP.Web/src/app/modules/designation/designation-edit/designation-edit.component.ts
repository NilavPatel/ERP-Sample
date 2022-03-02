import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { DesignationService } from 'src/app/core/services/designation.service';

@Component({
  selector: 'app-designation-edit',
  templateUrl: './designation-edit.component.html',
  styleUrls: ['./designation-edit.component.scss']
})
export class DesignationEditComponent implements OnInit {

  id: string | null = "";
  designationName: string = "";
  description: string = "";

  allViewChecked: boolean = false;
  allEditChecked: boolean = false;
  allAddChecked: boolean = false;
  allDeleteChecked: boolean = false;

  permissions: any[] = [];

  constructor(private router: Router,
    private route: ActivatedRoute,
    private designationService: DesignationService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.goToList();
    } else {
      this.getDesignationById();
    }
  }

  getDesignationById() {
    this.loaderService.showLoader();
    var req = {
      id: this.id
    };
    this.designationService.getDesignationById(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.designationName = value.data.name;
            this.description = value.data.description;
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
    this.router.navigateByUrl("/app/designation/list");
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      id: this.id,
      name: this.designationName,
      description: this.description
    }
    this.designationService.updateDesignation(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.getDesignationById();
          } else {
            this.messageService.add({ severity: 'error', detail: value.errorMessages[0] });
          }
        },
        error: (error: any) => {
          this.messageService.add({ severity: 'error', detail: error.message });
          this.loaderService.hideLoader();
        },
        complete: () => {
          this.loaderService.hideLoader();
        }
      });
  }
}
