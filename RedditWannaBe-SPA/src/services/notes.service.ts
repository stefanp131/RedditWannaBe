import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NoteForCreation } from 'src/models/NoteForCreation';
import { Observable } from 'rxjs';
import { NoteForRetrieval } from 'src/models/NoteForRetrieval';

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  private baseUrl = 'https://localhost:44366/api/topic/';

  constructor(private http: HttpClient) {}

  createNote(topicId: string, model: NoteForCreation) {
    return this.http.post(this.baseUrl + topicId + '/note', model);
  }

  getNotesForTopic(topicId: string): Observable<NoteForRetrieval[]> {
    return this.http.get<NoteForRetrieval[]>(this.baseUrl + topicId + '/note');
  }

  deleteNote(topicId: string, id: string) {
    return this.http.delete(this.baseUrl + topicId + '/note/' + id);
  }
}
