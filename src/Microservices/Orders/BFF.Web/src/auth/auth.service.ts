import { Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';

@Injectable()
export class AuthService {
  constructor(private configService: ConfigService) {}

  /**
   * Validates if the return URL is a valid relative path
   * Similar to DoesNextJsRelativePathExist in the C# code
   */
  isValidReturnUrl(returnUrl: string): boolean {
    if (!returnUrl) {
      return false;
    }

    // Check if it's a relative URL
    if (!returnUrl.startsWith('/')) {
      return false;
    }

    // Prevent open redirect attacks
    if (returnUrl.includes('//') || returnUrl.includes('\\')) {
      return false;
    }

    // You can add additional validation here
    // For example, check if the path exists in your NextJS routes
    // This is a simplified version - adjust based on your needs
    const validPaths = [
      '/v1/orders/view-all'
      // Add other valid paths
    ];

    // Allow any path starting with valid base paths
    const isValid = validPaths.some(
      (path) => returnUrl === path || returnUrl.startsWith(path + '/'),
    );

    return isValid;
  }

  /**
   * Helper method to call downstream APIs with the access token
   * Use this from your BFF services when calling the Orders Microservice API
   */
  async callDownstreamApi(
    accessToken: string,
    apiUrl: string,
    options: RequestInit = {},
  ): Promise<Response> {
    const headers = {
      Authorization: `Bearer ${accessToken}`,
      'Content-Type': 'application/json',
      ...options.headers,
    };

    return fetch(apiUrl, {
      ...options,
      headers,
    });
  }

  /**
   * Example method to call Orders API
   */
  async getOrders(accessToken: string): Promise<any> {
    const ordersApiUrl = this.configService.get('ORDERS_API_URL');
    const response = await this.callDownstreamApi(
      accessToken,
      `${ordersApiUrl}/api/orders`,
      { method: 'GET' },
    );

    if (!response.ok) {
      throw new Error(`Failed to fetch orders: ${response.statusText}`);
    }

    return response.json();
  }
}