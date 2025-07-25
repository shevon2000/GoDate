import { Component, input, ViewEncapsulation } from '@angular/core';
import { Member } from '../../../models/member';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-member-card',
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
})
export class MemberCardComponent {
  member = input.required<Member>();      // Get from parent component(member-list)

}
