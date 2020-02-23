import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { TopicForRetrieval } from 'src/models/TopicForRetrieval';
import { TopicsService } from 'src/services/topics.service';
import { DecodedToken } from 'src/models/DecodedToken';

@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.scss']
})
export class TopicComponent implements OnInit {

  @Input() topic: TopicForRetrieval;
  @Output() topicDeleted: EventEmitter<any> = new EventEmitter<any>();
  currentId: string;

  constructor(private topics: TopicsService) {
    const decodedToken = JSON.parse(localStorage.getItem('userInfo')) as DecodedToken;
    this.currentId = decodedToken.nameid;
  }

  ngOnInit() {
  }

  delete() {
    if (this.topic.createdById === this.currentId) {
      this.topics.deleteTopic(this.topic.id).subscribe(() => this.topicDeleted.emit());
    }
  }

}
