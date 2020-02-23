import { Component, OnInit } from '@angular/core';
import { TopicsService } from 'src/services/topics.service';
import { TopicForRetrieval } from 'src/models/TopicForRetrieval';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss', './home.component.mobile.scss']
})
export class HomeComponent implements OnInit {
  currentPage = 1;
  pageSize = 12;
  searchItemQuery: '';
  totalPages: 1;
  topics: TopicForRetrieval[];

  constructor(private topicsService: TopicsService) { }

  ngOnInit() {
    this.refreshList();
  }

  refreshList() {
    this.topicsService
      .getAll(this.currentPage, this.pageSize, this.searchItemQuery)
      .subscribe(response => {
        // tslint:disable-next-line: no-string-literal
        this.topics = response['topicsForRetrieval'];
        // tslint:disable-next-line: no-string-literal
        this.totalPages = response['totalPages'];
      });
  }
}
