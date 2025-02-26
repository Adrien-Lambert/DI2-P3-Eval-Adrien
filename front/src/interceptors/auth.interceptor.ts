import { HttpInterceptorFn } from "@angular/common/http";
import { environment } from "../environments/environment";

/**
 * Functional interceptor to add the `x-api-key` header to all outgoing HTTP requests.
 */
export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const modifiedReq = req.clone({
      setHeaders: {
        'x-api-key': environment.apiKey
      }
    });
  
    return next(modifiedReq);
}