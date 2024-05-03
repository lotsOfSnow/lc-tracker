/**
 * This file was auto-generated by openapi-typescript.
 * Do not make direct changes to the file.
 */


export interface paths {
  "/api/attempts": {
    get: operations["Attempts.GetAll"];
    post: operations["Attempts.Create"];
  };
  "/api/attempts/{id}": {
    get: operations["Attempts.Get"];
    put: operations["Attempts.Update"];
    delete: operations["Attempts.Delete"];
  };
  "/api/me/export": {
    get: operations["CurrentUser.Export"];
  };
  "/api/problems": {
    get: operations["Problems.GetAll"];
    post: operations["Problems.Create"];
  };
  "/api/problems/{id}": {
    get: operations["Problems.Get"];
    put: operations["Problems.Update"];
  };
}

export type webhooks = Record<string, never>;

export interface components {
  schemas: {
    Attempt: {
      /** Format: uuid */
      id?: string;
      /** Format: uuid */
      appUserId?: string;
      /** Format: uuid */
      problemId?: string;
      /**
       * Format: date
       * @example 2024-04-20
       */
      date?: string;
      note?: string | null;
      /** Format: int32 */
      minutesSpent?: number | null;
      difficulty?: components["schemas"]["Difficulty"];
      hasUsedHelp?: boolean;
      hasSolved?: boolean;
      isRecap?: boolean;
    };
    CreateAttemptRequest: {
      /** Format: uuid */
      problemId?: string;
      /** Format: int32 */
      minutesSpent?: number | null;
      /**
       * Format: date
       * @example 2024-04-20
       */
      date?: string;
      note?: string | null;
      hasUsedHelp?: boolean;
      hasSolved?: boolean;
      isRecap?: boolean;
      difficulty?: components["schemas"]["Difficulty"];
    };
    CreateProblemMethod: {
      name?: string | null;
      contents?: string | null;
    };
    CreateProblemRequest: {
      name?: string | null;
      /** Format: int32 */
      number?: number;
      url?: string | null;
      methods?: components["schemas"]["CreateProblemMethod"][] | null;
    };
    /**
     * Format: int32
     * @enum {integer}
     */
    Difficulty: 0 | 1 | 2 | 3 | 4;
    GetAllAttemptsResponse: {
      value?: components["schemas"]["Attempt"][] | null;
    };
    GetAllProblemsResponse: {
      value?: components["schemas"]["Problem"][] | null;
    };
    NotFoundResult: {
      /** Format: int32 */
      statusCode?: number;
    };
    Problem: {
      /** Format: uuid */
      id?: string;
      /** Format: uuid */
      appUserId?: string;
      /** Format: int32 */
      number?: number;
      name?: string | null;
      url?: string | null;
      /** Format: date-time */
      addedAt?: string;
      methods?: components["schemas"]["ProblemMethod"][] | null;
    };
    ProblemDetails: {
      type?: string | null;
      title?: string | null;
      /** Format: int32 */
      status?: number | null;
      detail?: string | null;
      instance?: string | null;
      [key: string]: unknown;
    };
    ProblemMethod: {
      name?: string | null;
      contents?: string | null;
    };
    UpdateAttemptRequest: {
      /** Format: uuid */
      problemId?: string;
      /** Format: int32 */
      minutesSpent?: number | null;
      /**
       * Format: date
       * @example 2024-04-20
       */
      date?: string;
      note?: string | null;
      hasUsedHelp?: boolean;
      hasSolved?: boolean;
      isRecap?: boolean;
      difficulty?: components["schemas"]["Difficulty"];
    };
    UpdateProblemRequest: {
      name?: string | null;
      /** Format: int32 */
      number?: number;
      url?: string | null;
    };
  };
  responses: never;
  parameters: never;
  requestBodies: never;
  headers: never;
  pathItems: never;
}

export type $defs = Record<string, never>;

export type external = Record<string, never>;

export interface operations {

  "Attempts.GetAll": {
    responses: {
      /** @description Success */
      200: {
        content: {
          "text/plain": components["schemas"]["GetAllAttemptsResponse"];
          "application/json": components["schemas"]["GetAllAttemptsResponse"];
          "text/json": components["schemas"]["GetAllAttemptsResponse"];
        };
      };
    };
  };
  "Attempts.Create": {
    requestBody?: {
      content: {
        "application/json": components["schemas"]["CreateAttemptRequest"];
        "text/json": components["schemas"]["CreateAttemptRequest"];
        "application/*+json": components["schemas"]["CreateAttemptRequest"];
      };
    };
    responses: {
      /** @description No Content */
      204: {
        content: never;
      };
      /** @description Bad Request */
      400: {
        content: {
          "text/plain": components["schemas"]["ProblemDetails"];
          "application/json": components["schemas"]["ProblemDetails"];
          "text/json": components["schemas"]["ProblemDetails"];
        };
      };
    };
  };
  "Attempts.Get": {
    parameters: {
      path: {
        id: string;
      };
    };
    responses: {
      /** @description Success */
      200: {
        content: {
          "text/plain": components["schemas"]["Attempt"];
          "application/json": components["schemas"]["Attempt"];
          "text/json": components["schemas"]["Attempt"];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "text/plain": components["schemas"]["NotFoundResult"];
          "application/json": components["schemas"]["NotFoundResult"];
          "text/json": components["schemas"]["NotFoundResult"];
        };
      };
    };
  };
  "Attempts.Update": {
    parameters: {
      path: {
        id: string;
      };
    };
    requestBody?: {
      content: {
        "application/json": components["schemas"]["UpdateAttemptRequest"];
        "text/json": components["schemas"]["UpdateAttemptRequest"];
        "application/*+json": components["schemas"]["UpdateAttemptRequest"];
      };
    };
    responses: {
      /** @description No Content */
      204: {
        content: never;
      };
      /** @description Bad Request */
      400: {
        content: {
          "text/plain": components["schemas"]["ProblemDetails"];
          "application/json": components["schemas"]["ProblemDetails"];
          "text/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "text/plain": components["schemas"]["NotFoundResult"];
          "application/json": components["schemas"]["NotFoundResult"];
          "text/json": components["schemas"]["NotFoundResult"];
        };
      };
    };
  };
  "Attempts.Delete": {
    parameters: {
      path: {
        id: string;
      };
    };
    responses: {
      /** @description No Content */
      204: {
        content: never;
      };
    };
  };
  "CurrentUser.Export": {
    responses: {
      /** @description Success */
      200: {
        content: never;
      };
    };
  };
  "Problems.GetAll": {
    responses: {
      /** @description Success */
      200: {
        content: {
          "text/plain": components["schemas"]["GetAllProblemsResponse"];
          "application/json": components["schemas"]["GetAllProblemsResponse"];
          "text/json": components["schemas"]["GetAllProblemsResponse"];
        };
      };
    };
  };
  "Problems.Create": {
    requestBody?: {
      content: {
        "application/json": components["schemas"]["CreateProblemRequest"];
        "text/json": components["schemas"]["CreateProblemRequest"];
        "application/*+json": components["schemas"]["CreateProblemRequest"];
      };
    };
    responses: {
      /** @description No Content */
      204: {
        content: never;
      };
      /** @description Bad Request */
      400: {
        content: {
          "text/plain": components["schemas"]["ProblemDetails"];
          "application/json": components["schemas"]["ProblemDetails"];
          "text/json": components["schemas"]["ProblemDetails"];
        };
      };
    };
  };
  "Problems.Get": {
    parameters: {
      path: {
        id: string;
      };
    };
    responses: {
      /** @description Success */
      200: {
        content: {
          "text/plain": components["schemas"]["Problem"];
          "application/json": components["schemas"]["Problem"];
          "text/json": components["schemas"]["Problem"];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "text/plain": components["schemas"]["NotFoundResult"];
          "application/json": components["schemas"]["NotFoundResult"];
          "text/json": components["schemas"]["NotFoundResult"];
        };
      };
    };
  };
  "Problems.Update": {
    parameters: {
      path: {
        id: string;
      };
    };
    requestBody?: {
      content: {
        "application/json": components["schemas"]["UpdateProblemRequest"];
        "text/json": components["schemas"]["UpdateProblemRequest"];
        "application/*+json": components["schemas"]["UpdateProblemRequest"];
      };
    };
    responses: {
      /** @description No Content */
      204: {
        content: never;
      };
      /** @description Bad Request */
      400: {
        content: {
          "text/plain": components["schemas"]["ProblemDetails"];
          "application/json": components["schemas"]["ProblemDetails"];
          "text/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "text/plain": components["schemas"]["NotFoundResult"];
          "application/json": components["schemas"]["NotFoundResult"];
          "text/json": components["schemas"]["NotFoundResult"];
        };
      };
    };
  };
}
