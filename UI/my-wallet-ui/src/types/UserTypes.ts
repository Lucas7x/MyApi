export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResponse {
    token: string;
}

export interface UserRegisterRequest {
    name: string;
    email: string;
    phone: string;
    password: string;
}

export interface UserRegisterResponse {
    id: number;
    name: string;
    email: string;
    phone: string;
}