import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { TopicForCreation } from 'src/models/TopicForCreation';
import { Observable } from 'rxjs';
import { TopicForRetrieval } from 'src/models/TopicForRetrieval';

@Injectable({
  providedIn: 'root'
})
export class TopicsService {
  private baseUrl = 'https://localhost:44366/api/topic';

  constructor(private http: HttpClient) {}

  createTopic(model: TopicForCreation) {
    return this.http.post(this.baseUrl, model);
  }

  getAll(currentPage?: number, pageSize?: number, searchItemQuery?: string ): Observable<TopicForRetrieval[]> {

    let params = new HttpParams();
    if (currentPage) {
      params = params.set('currentPage', currentPage.toString());
    }
    if (pageSize) {
      params = params.set('pageSize', pageSize.toString());
    }
    if (searchItemQuery) {
      params = params.set('searchItemQuery', searchItemQuery);
    }

    return this.http.get<TopicForRetrieval[]>(this.baseUrl, {params} );
  }

  deleteTopic(id: string) {
    return this.http.delete(this.baseUrl + '/' + id);
  }
}
