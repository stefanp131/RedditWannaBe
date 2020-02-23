import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { TopicsService } from 'src/services/topics.service';
import { TopicForCreation } from 'src/models/TopicForCreation';
import { DecodedToken } from 'src/models/DecodedToken';

@Component({
  selector: 'app-create-topic',
  templateUrl: './create-topic.component.html',
  styleUrls: ['./create-topic.component.scss']
})
export class CreateTopicComponent implements OnInit {
  title: string;
  description: string;
  @Output() topicCreated: EventEmitter<any> = new EventEmitter<any>();

  constructor(private topics: TopicsService) { }

  ngOnInit() {
  }

  createTopic() {
    const userInfoString = localStorage.getItem('userInfo');
    const userInfo = JSON.parse(userInfoString) as DecodedToken;
    const topic: TopicForCreation = {
      title: this.title,
      description: this.description,
      createdById: userInfo.nameid
    };

    this.topics.createTopic(topic).subscribe(() => this.topicCreated.emit());
  }
}
