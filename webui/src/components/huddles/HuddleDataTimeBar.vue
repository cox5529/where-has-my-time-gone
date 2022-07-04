<script setup lang="ts">
import { onUnmounted, ref } from 'vue';

import moment from 'moment';

const props = defineProps<{ start: string }>();

const totalSeconds = 24 * 60 * 60;

const offset = ref(0);

updateOffset();
const interval = setInterval(updateOffset, 1000);

function updateOffset() {
  const newOffset = convertDateToOffset(moment());
  offset.value = newOffset + 0.2;
}

function convertDateToOffset(date: moment.Moment): number {
  return date.diff(moment(props.start)) / 10 / totalSeconds;
}

onUnmounted(() => {
  clearInterval(interval);
});
</script>

<template>
  <div
    class="border-r-2 absolute h-full border-red-500"
    :style="{ left: `${offset}%` }"
  ></div>
</template>
