export type ApiPaths =
    | '/monitoring/api/tracing'
    | '/monitoring/api/all/tracing'
    | '/monitoring/api/credit/tracing'
    | '/monitoring/api/core/tracing'
    | '/monitoring/api/auth/tracing'
    | '/monitoring/api';

export interface GLOBAL_API {
    '/monitoring/api/all/tracing': {
        method: 'get';
        parameters: Pick<QueryParameters, 'begin' | 'end'>;
        body: GetTracingDTO;
    };
    '/monitoring/api/credit/tracing': {
        method: 'get';
        parameters: QueryParameters;
        body: GetTracingDTO;
    };
    '/monitoring/api/core/tracing': {
        method: 'get';
        parameters: QueryParameters;
        body: GetTracingDTO;
    };
    '/monitoring/api/auth/tracing': {
        method: 'get';
        parameters: QueryParameters;
        body: GetTracingDTO;
    };
    '/monitoring/api': {
        method: 'delete';
        parameters: QueryParameters;
        body: null;
    };
}

export type ServiceEnum = 0 | 1 | 2; // int32

export interface TimeSpan {
    ticks?: number; // int64
    days?: number; // int32
    hours?: number; // int32
    milliseconds?: number; // int32
    microseconds?: number; // int32
    nanoseconds?: number; // int32
    minutes?: number; // int32
    seconds?: number; // int32
    totalDays?: number; // double
    totalHours?: number; // double
    totalMilliseconds?: number; // double
    totalMicroseconds?: number; // double
    totalNanoseconds?: number; // double
    totalMinutes?: number; // double
    totalSeconds?: number; // double
}

export interface TracingDTO {
    time?: TimeSpan;
    created_At?: string; // date-time
    route?: string | null;
    method?: string | null;
    description?: string | null;
    statusCode?: number; // int32
    type?: TracingEnum /* int32 */;
    service?: ServiceEnum /* int32 */;
}

export type TracingEnum = 0 | 1; // int32

export interface QueryParameters {
    begin?: string /* date-time */;
    end?: string /* date-time */;
    type?: ServiceEnum;
}

export interface GetTracingDTO extends TracingDTO {
    id?: string;
}
