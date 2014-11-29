using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos_VM {
	public class Memory {
		public byte[] ram;
		public List<DeviceMappedRegion> mappedRegions = new List<DeviceMappedRegion>();

		public Memory(int length) {
			ram = new byte[length];
		}

		public void MapRegion(DeviceMappedRegion dev) {
			mappedRegions.Add(dev);
		}

		public byte this[uint index] {
			get {
				if (isMapped(index))
					return getDevice(index).Read(index);
				else
					return ram[index];
			}
			set {
				if (isMapped(index))
					getDevice(index).Write(index, value);
				else
					ram[index] = value;
			}
		}

		public void setValue(uint index, byte value) {
			if (isMapped(index))
				getDevice(index).Write(index, value);
			else
				ram[index] = value;
		}

		public bool isMapped(uint address) {
			foreach (DeviceMappedRegion dev in mappedRegions)
				if (dev.Base <= address && dev.Limit > address)
					return true;
			return false;
		}

		private DeviceMappedRegion getDevice(uint address) {
			foreach (DeviceMappedRegion dev in mappedRegions)
				if (dev.Base <= address && dev.Limit > address)
					return dev;
			return null;
		}
	}
}
