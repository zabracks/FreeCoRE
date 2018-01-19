﻿
export interface ErrorResponse {
    Message: string;
}

export interface User {
    UserId: string;    
    Email: string;
}

export interface Instance {
    InstanceId: string;
    Name: string;
    AccountId: string;
}

export interface ProjectListing {
    ProjectId: string;
    Name: string;
    Description: string;
    InstanceId: string;
}

export interface CreateProjectRequest {
    Name: string;
    Description: string;
    InstanceId: string;
}

export interface Routine {
    RoutineId: string;
    Name: string;
    Description: string;
    ProjectId: string;
    BlockId: string;
    ExecutionMethod: string;
}
