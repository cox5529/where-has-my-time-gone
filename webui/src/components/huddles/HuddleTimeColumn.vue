<script setup lang="ts">
import HuddleTimeEntry from './HuddleTimeEntry.vue';
import { computed } from 'vue';

import moment from 'moment';

const props = defineProps<{ timeCount: number }>();

const times = computed(() => {
  const arr: string[] = [];
  const minuteDelta = (24 * 60) / props.timeCount;
  for (let i = 0; i < props.timeCount; i++) {
    const time = moment()
      .startOf('day')
      .add(i * minuteDelta, 'minute');
    arr.push(time.format('LT'));
  }

  return arr;
});
</script>

<template>
  <div class="h-full w-32 border-r-4 flex flex-col">
    <HuddleTimeEntry v-for="time of times" :key="time" :time="time" />
  </div>
</template>
