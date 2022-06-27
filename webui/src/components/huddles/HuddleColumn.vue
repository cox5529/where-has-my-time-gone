<script setup lang="ts">
import { computed } from 'vue';

import moment from 'moment';

import type {
  FetchHuddlesProfileResponse,
  Huddle,
} from '@/models/responses/fetch-huddles-response';

type AnnotatedHuddle = Huddle & { startPosition: number; height: number };

const props = defineProps<{
  profile: FetchHuddlesProfileResponse;
  start: string;
  height: number;
  timeCount: number;
}>();

const totalSeconds = 24 * 60 * 60;

const huddles = computed<AnnotatedHuddle[]>(() =>
  props.profile.huddles.map((x) => {
    const start = convertDateToOffset(x.start);
    const end = convertDateToOffset(x.end);

    return { ...x, startPosition: start, height: end - start };
  })
);

function convertDateToOffset(date: string): number {
  return (
    ((moment(date).diff(moment(props.start)) / 1000) * props.height) /
    totalSeconds
  );
}
</script>

<template>
  <div class="h-full flex flex-col relative border-r w-32">
    <div
      class="flex-grow border-t border-dashed"
      v-for="i in timeCount"
      :key="i"
    ></div>
    <div
      class="bg-blue-500 absolute w-8 left-1/2 -translate-x-1/2 z-0"
      v-for="(huddle, index) of huddles"
      :key="index"
      :style="{
        top: `${huddle.startPosition}px`,
        height: `${huddle.height}px`,
      }"
    ></div>
  </div>
</template>
