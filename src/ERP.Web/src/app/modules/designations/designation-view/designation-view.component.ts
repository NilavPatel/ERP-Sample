import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { DesignationService } from 'src/app/core/services/designation.service';

@Component({
  selector: 'app-designation-view',
  templateUrl: './designation-view.component.html',
  styleUrls: ['./designation-view.component.scss']
})
export class DesignationViewComponent implements OnInit {

  id: string | null = "";
  designationName: string = "";
  description: string = "";

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
      },
      complete: () => {
        this.loaderService.hideLoader();
      }
    });
  }

  goToList() {
    this.router.navigateByUrl("/app/designations/list");
  }

}
