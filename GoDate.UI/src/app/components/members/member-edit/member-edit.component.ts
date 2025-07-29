import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { Member } from '../../../models/member';
import { AccountService } from '../../../services/account.service';
import { MembersService } from '../../../services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PhotoEditorComponent } from "../photo-editor/photo-editor.component";

@Component({    
  selector: 'app-member-edit',
  imports: [TabsModule, FormsModule, PhotoEditorComponent],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {  // Template is considered as a child of the component, @ViewChild allow access template reference.
  @ViewChild('editForm') editForm?: NgForm;           // Optional because component initilized first before the template.
  @HostListener('window:beforeunload', ['$event'])
unloadNotification($event: BeforeUnloadEvent) {
  if (this.editForm?.dirty) {
    $event.returnValue = 'You have unsaved changes. Are you sure you want to leave?';
  }
}


  member?: Member;
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);
  private toastr = inject(ToastrService)

  ngOnInit(): void {
      this.loadMember();
  }

  loadMember() {
    const user = this.accountService.currentUser();

    if(!user) return;
    this.memberService.getMember(user.userName).subscribe({
      next: member => this.member = member
    })
  }

  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: () => {
        this.toastr.success('Profile updated successfully');
        this.editForm?.reset(this.member);
      }
    })
  }

  onMemberChange(event: Member) {
    this.member = event;
  }
  
}