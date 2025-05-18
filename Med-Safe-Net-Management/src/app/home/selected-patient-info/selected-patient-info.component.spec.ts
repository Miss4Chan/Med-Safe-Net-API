import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedPatientInfoComponent } from './selected-patient-info.component';

describe('SelectedPatientInfoComponent', () => {
  let component: SelectedPatientInfoComponent;
  let fixture: ComponentFixture<SelectedPatientInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectedPatientInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectedPatientInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
