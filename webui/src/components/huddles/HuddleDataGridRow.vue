<script setup lang="ts">
import HuddleDataGridRowEntry from './HuddleDataGridRowEntry.vue';
import { computed } from 'vue';

import moment from 'moment';

import type { FetchHuddlesProfileResponse } from '@/models/responses/fetch-huddles-response';

export type Huddle = {
  start: string;
  end: string;
  left: number;
  width: number;
};

const props = defineProps<{
  date: string;
  timeSlices: number;
  profile: FetchHuddlesProfileResponse;
}>();

const secondsPerDay = 24 * 60 * 60;

const huddles = computed(() => {
  const startOfDay = moment(props.date).startOf('day');
  return props.profile.huddles.map((x) => {
    const startDelta =
      (moment(x.start).diff(startOfDay, 'seconds') / secondsPerDay) * 100;
    const endDelta =
      (moment(x.end).diff(startOfDay, 'seconds') / secondsPerDay) * 100;

    const start = moment(x.start).format('LT');
    const end = moment(x.end).format('LT');

    return {
      start,
      end,
      left: startDelta,
      width: endDelta - startDelta,
    } as Huddle;
  });
});
</script>

<template>
  <div class="h-36 border-b border-dashed w-[144rem]">
    <HuddleDataGridRowEntry
      v-for="(huddle, index) in huddles"
      :key="index"
      :huddle="huddle"
    />
  </div>
</template>
