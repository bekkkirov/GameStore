import {Image} from "./image";

export interface Comment {
    id: number;
    authorUserName: string;
    authorProfileImage: Image,
    timeStamp: string;
    body: string;
    replies: Comment[];
}