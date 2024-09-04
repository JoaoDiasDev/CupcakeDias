import { User } from './user.model';

export interface Role {
  roleId: string;
  roleName: string;
  users?: User[];
}
