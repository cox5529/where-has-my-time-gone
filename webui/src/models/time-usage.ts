import type { TimeUsageType } from './time-usage-type';

export interface TimeUsage {
  id: number;
  start: string;
  end?: string;
  durationSeconds?: number;
  type: TimeUsageType;
}
