import {
  Body,
  Controller,
  Get,
  HttpException,
  HttpStatus,
  Param,
  Post,
  Req,
  UseGuards,
} from '@nestjs/common';
import { Request } from 'express';
import { AuthService } from '../auth/auth.service';
import { SessionAuthGuard } from '../auth/guards/session-auth.guard';

@Controller('api/orders')
export class OrdersController {
  constructor(private readonly authService: AuthService) {}

  @Get('view-all')
  async getOrders(@Req() req: Request) {
      const accessToken = (req.session as any)?.AccessToken;

      // const user = req.user as any;
      // const accessToken = user?.tokens?.accessToken;

      console.log('Accessing getOrders with access token:', accessToken ? accessToken.substring(0, 10) + '...' : 'none');

      if (!accessToken) {
        throw new HttpException(
          'Access token not found',
          HttpStatus.UNAUTHORIZED,
        );
      }

      const ordersMicroserviceApiUrl = process.env.ORDERS_MICROSERVICE_API_URL;
      const graphQLQuery = {
        query: '{ orderReport { nodes { orderId dateOfOrder totalAmount paymentMethod invoiceNumber numberOfItems dispatchStatus customerName } } }'
      };

      const response = await this.authService.callDownstreamApi(
        accessToken,
        `${ordersMicroserviceApiUrl}/graphql`,
        {
          method: 'POST',
          body: JSON.stringify(graphQLQuery),
        },
      );

      if (!response.ok) {
        throw new HttpException(
          'Failed to fetch order',
          response.status,
        );
      }

      return response.json();
  }

  @Get(':id')
  async getOrderById(@Param('id') id: string, @Req() req: Request) {
    try {
      const user = req.user as any;
      const accessToken = user?.tokens?.accessToken;

      if (!accessToken) {
        throw new HttpException(
          'Access token not found',
          HttpStatus.UNAUTHORIZED,
        );
      }

      const ordersMicroserviceApiUrl = process.env.ORDERS_MICROSERVICE_API_URL;
      const response = await this.authService.callDownstreamApi(
        accessToken,
        `${ordersMicroserviceApiUrl}/api/orders/${id}`,
        { method: 'GET' },
      );

      if (!response.ok) {
        throw new HttpException(
          'Failed to fetch order',
          response.status,
        );
      }

      return response.json();
    } catch (error) {
      throw new HttpException(
        error.message || 'Failed to fetch order',
        error.status || HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }

  @Post()
  async createOrder(@Body() orderData: any, @Req() req: Request) {
    try {
      const user = req.user as any;
      const accessToken = user?.tokens?.accessToken;

      if (!accessToken) {
        throw new HttpException(
          'Access token not found',
          HttpStatus.UNAUTHORIZED,
        );
      }

      const ordersMicroserviceApiUrl = process.env.ORDERS_MICROSERVICE_API_URL;
      const response = await this.authService.callDownstreamApi(
        accessToken,
        `${ordersMicroserviceApiUrl}/api/orders`,
        {
          method: 'POST',
          body: JSON.stringify(orderData),
        },
      );

      if (!response.ok) {
        throw new HttpException(
          'Failed to create order',
          response.status,
        );
      }

      return response.json();
    } catch (error) {
      throw new HttpException(
        error.message || 'Failed to create order',
        error.status || HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }
}