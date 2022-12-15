import {Image} from "./image";

export interface AuthResult {
    token: string;
    userName: string;
    firstName: string;
    lastName: string;
    profileImage: Image;
}