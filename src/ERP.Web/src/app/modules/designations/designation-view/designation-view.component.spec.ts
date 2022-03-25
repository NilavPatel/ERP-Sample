import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignationViewComponent } from './designation-view.component';

describe('DesignationViewComponent', () => {
  let component: DesignationViewComponent;
  let fixture: ComponentFixture<DesignationViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DesignationViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignationViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
