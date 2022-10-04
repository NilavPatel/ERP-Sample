import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoaderService } from 'src/app/core/services/loader.service';
import { DepartmentService } from 'src/app/modules/departments/shared/department.service';

@Component({
  selector: 'app-department-edit',
  templateUrl: './department-edit.component.html',
  styleUrls: ['./department-edit.component.scss']
})
export class DepartmentEditComponent implements OnInit {

  id: string | null = "";
  departmentName: string = "";
  description: string = "";

  constructor(private router: Router,
    private route: ActivatedRoute,
    private departmentService: DepartmentService,
    private loaderService: LoaderService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.goToList();
    } else {
      this.getDepartmentById();
    }
  }

  getDepartmentById() {
    this.loaderService.showLoader();
    var req = {
      id: this.id
    };
    this.departmentService.getDepartmentById(req).subscribe({
      next: (value: any) => {
        if (value && value.isValid) {
          if (value.data) {
            this.departmentName = value.data.name;
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
    this.router.navigateByUrl("/app/departments/list");
  }

  onSubmit() {
    this.loaderService.showLoader();
    var req = {
      id: this.id,
      name: this.departmentName,
      description: this.description
    }
    this.departmentService.updateDepartment(req)
      .subscribe({
        next: (value: any) => {
          if (value && value.isValid) {
            this.messageService.add({ severity: 'success', detail: value.successMessages[0] });
            this.getDepartmentById();
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
