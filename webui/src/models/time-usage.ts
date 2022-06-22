import type { TimeUsageType } from './time-usage-type';

export interface TimeUsage {
  start: string;
  end?: string;
  duration?: number;
  type: TimeUsageType;
}
