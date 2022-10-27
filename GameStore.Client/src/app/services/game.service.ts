import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Game} from "../models/game";
import {API_BASE_URL} from "../extensions/injection-token";
import {PlatformType} from "../models/platformType";
import {Genre} from "../models/genre";
import {GameCreate} from "../models/game-create";
import {GameSearchOptions} from "../models/game-search-options";

@Injectable({
    providedIn: 'root'
})
export class GameService {
    constructor(private http: HttpClient,
                @Inject(API_BASE_URL) private apiUrl: string) {
    }

    getGames() {
        return this.http.get<Game[]>(this.apiUrl + "games");
    }

    getGame(gameKey: string) {
        return this.http.get<Game>(this.apiUrl + `game/${gameKey}`);
    }

    getGenres() {
        return this.http.get<Genre[]>(this.apiUrl + "games/genres");
    }

    getPlatforms() {
        return this.http.get<PlatformType[]>(this.apiUrl + "games/platforms");
    }

    addGame(game: GameCreate) {
        return this.http.post<Game>(this.apiUrl + "games/new", game);
    }

    editGame(gameKey: string, game: GameCreate) {
        return this.http.put(this.apiUrl + `games/update/${gameKey}`, game);
    }

    setImage(gameKey: string, image) {
        let headers = new HttpHeaders();
        headers.append('Content-Disposition', 'multipart/form-data');

        let formData = new FormData();
        formData.append('image', image);

        return this.http.post(this.apiUrl + `games/image/${gameKey}`, formData, {headers})
    }

    deleteGame(gameId: number) {
        return this.http.delete(this.apiUrl + `games/remove/${gameId}`);
    }

    search(gameSearchOptions: GameSearchOptions) {
        let params = new HttpParams();

        if(gameSearchOptions.namePattern) {
            params = params.append("namePattern", gameSearchOptions.namePattern);
        }

        if (gameSearchOptions.genreIds) {
            for (let id of gameSearchOptions.genreIds) {
                params = params.append("genreIds", id);
            }
        }

        return this.http.get<Game[]>(this.apiUrl + "games/search", {params: params});
    }
}
