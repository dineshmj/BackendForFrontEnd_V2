export interface Claim {
  type: string;
  value: string;
}

// --- Menu Interfaces ---

export interface MenuItem {
  taskName: string;
  urlRelativePath: string;
  iconName: string;
  managementAreaName?: string;
  microserviceName?: string;
  baseURL?: string;
}

export interface ManagementArea {
  name: string;
  menuItems: MenuItem[];
}

export interface Microservice {
  name: string;
  baseURL: string;
  managementAreas: ManagementArea[];
}

export interface MenuResponse {
  microservices: Microservice[];
}