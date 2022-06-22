import type { TimeUsage } from './time-usage';

export interface Person {
  id: number;
  name: string;
  image?: string;
  usages: TimeUsage[];
}
