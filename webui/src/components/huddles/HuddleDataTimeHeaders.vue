<script setup lang="ts">
import { computed } from 'vue';

import moment from 'moment';

const props = defineProps<{
  timeSlices: number;
}>();

const times = computed(() => {
  const time = moment().startOf('day');
  const delta = (24 * 60 * 60) / props.timeSlices;

  const arr: string[] = [];
  for (let i = 0; i < props.timeSlices; i++) {
    arr.push(time.format('LT'));
    time.add(delta, 'seconds');
  }

  return arr;
});
</script>

<template>
  <div>
    <div class="flex h-24 border-b-4 sticky top-0 bg-slate-900">
      <div
        class="flex w-24 justify-start border-r border-r-dashed px-2 flex-shrink-0"
        v-for="(time, index) in times"
        :key="index"
      >
        <span class="rotate-90 origin-top-left translate-x-4">{{ time }}</span>
      </div>
    </div>
    <div class="flex h-[calc(100%-6rem)]">
      <div
        class="border-r w-24 border-dashed"
        v-for="j in timeSlices"
        :key="j"
      ></div>
    </div>
  </div>
</template>
